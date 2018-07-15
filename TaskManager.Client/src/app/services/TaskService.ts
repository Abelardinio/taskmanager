import { TaskInfo } from '../models/TaskInfo';
import {TaskFilter, TakeType} from '../models/TaskFilter'
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class TaskService {

    private _headers : HttpHeaders; 

    constructor(private _httpClient: HttpClient) { 
            this._headers = new HttpHeaders();
            this._headers = this._headers.append('Content-Type', 'application/json');
        }

    public Add(taskInfo: TaskInfo): Observable<Object> {
        return this._httpClient.post(environment.API_URL + '/task', JSON.stringify(taskInfo),
            { headers: this._headers });
    }

    public Get(filter: TaskFilter) : Observable<Object>{
        let params = new HttpParams();
        params = params.append('taskId', filter.taskId.toString());
        params = params.append('count', filter.count.toString());
        params = params.append('type', filter.type.toString());


        return this._httpClient.get(environment.API_URL + '/task', { headers: this._headers , params: params });
    }

    public Complete(taskId:number): Observable<Object> {
        return this._httpClient.put(environment.API_URL + '/task/'+ taskId + '/complete', {});
    }

    public Delete(taskId:number): Observable<Object> {
        return this._httpClient.delete(environment.API_URL + '/task/'+ taskId, {} );
    }
}