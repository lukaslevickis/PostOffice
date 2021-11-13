import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Parcel } from '../models/parcel';

@Injectable({
  providedIn: 'root'
})
export class ParcelService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
   }

  public getParcels(): Observable<Parcel[]> {
    return this.http.get<Parcel[]>("https://localhost:5002/api/parcel");
  }

  public addParcel(parcel: Parcel): Observable<Parcel> {
    return this.http.post<Parcel>("https://localhost:5002/api/parcel", parcel);
  }

  public deleteParcel(id: number): Observable<unknown> {
    return this.http.delete(`${"https://localhost:5002/api/parcel"}/${id}`);
  }

  public updateParcel(parcel: Parcel): Observable<Parcel> {
    return this.http.put<Parcel>(`${"https://localhost:5002/api/parcel"}/${parcel.id}`, parcel);
  }

  public getPostParcels(parcelId: number): Observable<Parcel[]> {
    return this.http.get<Parcel[]>(`${"https://localhost:5002/api/parcel/post"}/${parcelId}`);
  }
}