import { Injectable } from "@angular/core";
import { DevicesDataStore } from "../datastores/devices.datastore";

@Injectable({providedIn: 'root'})
export class DevicesService {
    constructor(private readonly dataStore: DevicesDataStore) {}
}