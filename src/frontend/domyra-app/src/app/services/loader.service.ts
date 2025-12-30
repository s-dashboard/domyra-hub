import { Injectable } from "@angular/core";
import { BehaviorSubject, defer, finalize, Observable, take } from "rxjs";

@Injectable({providedIn: 'root'})
export class LoaderService {
    private readonly _loader$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    private readonly _loadingText$: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

    loading$ = this._loader$.asObservable();
    loadingText$ = this._loadingText$.asObservable();

    wrap<T>(source$: Observable<T>, loadingText: string = "Loading..."): Observable<T> {
        return defer(() => {
            this._loader$.next(true);
            
            this._loadingText$.next(loadingText);
            return source$.pipe(finalize(() => {
                this._loader$.next(false);
                this._loadingText$.next(null);
            }));
        });
    }
}