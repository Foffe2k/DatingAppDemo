import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  //implementerar OnInit som är en lifecycle event som sker efter att alla coola balla undersystem har gjort sitt
  http = inject(HttpClient);
  title = 'DatingApp';
  users: any;

  //Konvention säger att funktioner läggs under class properties även i 
  //Angular trots att autogenererationen lägger den under signaturen
  ngOnInit(): void { //Gör http-anropet i den här funktionen!
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log('Requst has completed')
    })
  } 
}
