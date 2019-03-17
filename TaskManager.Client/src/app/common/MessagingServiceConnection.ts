import * as signalR from '@aspnet/signalr';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { LocalStorageAccessor } from './LocalStorageAccessor';

@Injectable({
    providedIn: 'root',
})

/**
 * Connection to 'MessagingService' which provides real time data update
 */
export class MessagingServiceConnection {
    private connection: signalR.HubConnection;
    private isStarted: boolean;

    public constructor(private _localStorageAccessor: LocalStorageAccessor){
    }

    /**
     * Init connection.
     *
     * @param connectionURl relative url to the hub.
     */
    public init(connectionURl: string) {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(environment.MESSAGING_URL + connectionURl, { accessTokenFactory: () => this._localStorageAccessor.token })
            .build();
    }

    /**
     * Registers a handler that will be invoked when the hub method with the specified method name will be invoked
     *
     * @param methodName the name of the hub method
     * @param handler handler to be invoked
     */
    public on(methodName: string, handler: (data: any) => void) {
        this.connection.on(methodName, data => {
            handler(data);
        });

        this.start();
    }

    /**
     * Starts the connection
     */
    public start() {
        if (!this.isStarted) {
            this.connection.start();
            this.isStarted = true;
        }
    }
}
