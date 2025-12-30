import { Injectable } from "@angular/core";
import { DevicesDataStore } from "../datastores/devices.datastore";
import { Device } from "../models/device.model";

@Injectable({providedIn: 'root'})
export class DevicesService {
    constructor(private readonly dataStore: DevicesDataStore) {
    }

    activate(device: Device): void {
        let activateText = `Activating device "${device.name}"`
        if(device.state === 1) {
            activateText = `Deactivating device "${device.name}"`
        }
        
        this.dataStore.activate(device.id!, activateText); 
    }
}