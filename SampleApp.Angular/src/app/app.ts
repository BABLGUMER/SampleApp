import { Component, signal } from '@angular/core';
import { HomeComponent } from './home/home';

@Component({
    selector: 'app-root',
    imports: [HomeComponent],
    templateUrl: './app.html',
    styleUrl: './app.scss'
})
export class App {
    protected readonly title = signal('SampleApp.Angular');
}