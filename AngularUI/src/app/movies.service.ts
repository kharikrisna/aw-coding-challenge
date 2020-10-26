import { Injectable } from '@angular/core';
import{ HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Movie } from './Movie';
import { tap, map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  apiUrl: string=environment+"{0}";
  firstPage: any;
  lastPage: any;
  prevPage: any;
  nextPage: any;

  constructor(private httpClient: HttpClient) { }

  public getMovies(params):Observable<any>{
    return this.httpClient.get<Movie[]>(`https://localhost:44373/api/Movies`,{params});
    // {
    //   observe:'response'
    // }).pipe(tap(res=>{
    //   this.retrieve_pagination_links(res)
    // }));
  }

  public retrieve_pagination_links(response){
    const linkHeader = this.parse_link_header(response.headers.get('Link'));
    this.firstPage = linkHeader["first"];
    this.lastPage =  linkHeader["last"];
    this.prevPage =  linkHeader["prev"];
    this.nextPage =  linkHeader["next"];
}
parse_link_header(header) {
  if (header.length == 0) {
    return ;
  }

  let parts = header.split(',');
  var links = {};
  parts.forEach( p => {
    let section = p.split(';');
    var url = section[0].replace(/<(.*)>/, '$1').trim();
    var name = section[1].replace(/rel="(.*)"/, '$1').trim();
    links[name] = url;

  }); 
  return links;
}
}
