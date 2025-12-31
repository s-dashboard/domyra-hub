import { Type } from "@angular/core";

export type WidgetKind = 'angular' | 'web-component' | 'html';

export interface Widget {
  alias: string;
  kind: WidgetKind;
  elementName?: string;          // for web components
  scriptUrl?: string;            // for web components
  html?: string;                 // for static widgets
  cols: number;
  rows: number;
}

export interface WidgetDefinition extends Widget {
  component: Type<unknown> | null; // for internal Angular widgets
}

export const widgetColumns: string[] = ['alias', 'state'];