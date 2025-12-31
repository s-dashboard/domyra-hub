import { Injectable, Type } from "@angular/core";
import { WidgetDefinition } from "../models/widget.model";
import { WidgetResponse } from "../responses/widget.response";
import { ClockWidget } from "../components/widgets/clock-widget/clock-widget";

@Injectable({ providedIn: 'root' })
export class WidgetLoaderService {

  registry: Record<string, Type<unknown>> = {
      clock: ClockWidget
      // status: StatusWidgetComponent,
  };

  async load(dto: WidgetResponse): Promise<WidgetDefinition> {
    switch (dto.kind) {

      case 'angular':
        return {
          alias: dto.alias,
          kind: 'angular',
          component: this.resolveInternalComponent(dto.alias),
          cols: dto.cols,
          rows: dto.rows,
        };

      case 'web-component':
        await this.loadScriptOnce(dto.scriptUrl);

        return {
          alias: dto.alias,
          kind: 'web-component',
          component: null,
          elementName: dto.elementName,
          cols: dto.cols,
          rows: dto.rows,
        };

      case 'html':
        return {
          alias: dto.alias,
          kind: 'html',
          component: null,
          html: dto.html,
          cols: dto.cols,
          rows: dto.rows,
        };

      default:
        throw new Error(`Unsupported widget kind: ${dto.kind}`);
    }
  }

  private resolveInternalComponent(alias: string): Type<unknown> {
    const component = this.registry[alias];
    if (!component) {
      throw new Error(`Unknown internal widget: ${alias}`);
    }

    return component;
  }

  private loadScriptOnce(url: string | undefined): Promise<void> {
    
    if(!url) {
        return Promise.reject();
    }

    if (document.querySelector(`script[src="${url}"]`)) {
      return Promise.resolve();
    }

    return new Promise<void>((resolve, reject) => {
      const script = document.createElement('script');
      script.src = url;
      script.type = 'module';
      script.onload = () => resolve();
      script.onerror = () => reject(`Failed to load ${url}`);
      document.body.appendChild(script);
    });
  }
}