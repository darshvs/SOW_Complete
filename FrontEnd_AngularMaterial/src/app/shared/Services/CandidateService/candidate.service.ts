import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  apiUrl = environment.apiUrl;
  baseUrl: string = this.apiUrl + "/Candidate";
  private header = new HttpHeaders({ "content-type": "application/json" });

  constructor(private http: HttpClient) {}

  PostCandidateDuplicateCheck(data: any): Observable<any> {
    let DATA = { candidates: data };
    return this.http
      .post(`${this.baseUrl}/ImportData`, DATA, {
        headers: this.header,
        
      })
      
  }
  GetAllCandidatesData(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}`);
  }
  PostCandidateData(data: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}`, data);
  }
  DeleteCandidateData(id: any): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${id}`);
  }
  UpdateCandidateData(id: any, data: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, data);
  }
  GetCandidateById(id: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }
  GetCandidateByDate(startDate: any, endDate: any): Observable<any> {
    return this.http.get<any>(
      `${this.baseUrl}/GetDate?StartDate=${startDate}&EndDate=${endDate}`
    );
  }
}
