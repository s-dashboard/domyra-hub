export interface Device {
  id?: number;
  name: string;
  state: number;
  description?: string;
}

export const deviceColumns: string[] = ['id', 'name', 'state'];
