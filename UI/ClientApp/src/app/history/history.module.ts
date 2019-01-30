import { HistoryComponent } from './history.component';
import { CommonModule } from '@angular/common';
import { NgModule, InjectionToken } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    { path: '', component: HistoryComponent }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        HistoryComponent
    ]
})
export class HistoryModule { }


