import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from '../Movie';
import { MoviesService } from '../movies.service';


@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {

 movies: any;
 currentIndex = -1;
 title = '';
 sort='';

 page = 1;
 count = 0;
 pageSize = 10;
 
  constructor(private movieSer:MoviesService ) {
    
   }

  ngOnInit() {

    this.retriveMovies();
  
  }

  retriveMovies(){
    const params = this.getRequestParams(this.title, this.sort, this.page, this.pageSize);
    this.movieSer.getMovies(params).subscribe(
      response => {
        const { movies, totalItems } = response;
        this.movies = response;
        this.count = response.size;
        console.log(response.size);
      },
      error => {
        console.log(error);
      });
  }
  handlePageChange(event) {
    this.page = event;
    console.log(this.page);
    this.retriveMovies();
  }

  Sorting(event){
    
    this.sort=event.target.innerText;
    this.retriveMovies();
      console.log(event.target.innerText);
  }
  handlePageSizeChange(event) {
    this.pageSize = event.target.value;
    this.page = 1;
    this.retriveMovies();
  }
  getRequestParams(searchTitle,sorting, page, pageSize) {
    // tslint:disable-next-line:prefer-const
    let params = {};

    if (searchTitle) {
      params[`SearchByTtile`] = searchTitle;
    }
    if(sorting){
      params[`SortColumn`]=sorting;
    }
 
    if (page) {
      params[`page`] = page - 1;
    }

    if (pageSize) {
      params[`size`] = pageSize;
    }

    return params;
  } 

}
