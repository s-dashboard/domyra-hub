import { Injectable } from "@angular/core";
import { catchError, map, Observable } from "rxjs";
import { DeviceResponse } from "../responses/device.response";
import { HttpClient, HttpResponse } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { SaveDeviceRequest } from "../requests/savedevice.request";
import { ErrorService } from "../services/error.service";

@Injectable({providedIn: 'root'})
export class DevicesDataStore {

    private source: string = `${environment.apiUri}/devices`; 

    constructor(private readonly http: HttpClient,
        private readonly errors: ErrorService
    ) {}
    
    fetchSingle(id: number): Observable<DeviceResponse> {
        return this.http.get<DeviceResponse>(`${this.source}/${id}`);
    }

    fetchAll(): Observable<DeviceResponse[]> {
        return this.http.get<DeviceResponse[]>(`${this.source}`);
    }

    save(request: SaveDeviceRequest): Observable<DeviceResponse|null> {
        const id = request.body?.id;

        if (id) {
            // Update existing device
            return this.update(request); 
        }

        // Create new device
        return this.add(request);
    }

    private add(request: SaveDeviceRequest): Observable<DeviceResponse> {
        return this.http.post<DeviceResponse>(
            this.source,
            request
        ).pipe(
            catchError(err => this.errors.handleBadRequest(err))
        );
    }

    private update(request: SaveDeviceRequest): Observable<null> {
        const id = request.body?.id;

        return this.http.put<null>(
            `${this.source}/${id}`,
            request, {
                observe: 'response'
            }
        ).pipe(
            map((res: HttpResponse<void>) => {
                // Explicit success handling
                if (res.status === 204) {
                return null;
                }
                return null;
            }),
            catchError(err => this.errors.handleBadRequest(err))
        );
    }
}