import { Widget } from '../models/widget.model';

export interface WidgetResponse extends Widget {
    state: number;
}
