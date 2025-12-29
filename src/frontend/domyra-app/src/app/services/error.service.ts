import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { throwError } from "rxjs";

@Injectable({providedIn: 'root'})
export class ErrorService {
    handleBadRequest(error: HttpErrorResponse) {
        if (error.status === 400) {
            // backend validation error, domain error, etc.
            return throwError(() => error.error);
        }

        // rethrow all other errors
        return throwError(() => error);
    }
}