import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Post } from 'src/app/models/post';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  @Output() onPostUpdated = new EventEmitter();
  private postService: PostService;
  posts: Post[];
  update: boolean = false;
  postId: number = null as any;
  town: string = '';
  capacity: number = null as any;
  code: string = '';

  postData = {} as Post;
  postForm = this.formBuilder.group({
    town: '',
    capacity: null,
    code: '',
  });

  constructor(postService: PostService, private formBuilder: FormBuilder) { 
    this.postService = postService;
   }

  ngOnInit(): void {
    this.postService.getPosts().subscribe((postsFromApi) =>{
      let orderedPosts = postsFromApi.sort((a, b) => (a.code.toLowerCase() > b.code.toLowerCase()) ? 1 : -1);
      this.posts = orderedPosts;
    })
  }

  addPost(): void {
    this.postData.id = this.postForm.value.id;
    this.postData.town = this.postForm.value.town;
    this.postData.capacity = this.postForm.value.capacity;
    this.postData.code = this.postForm.value.code;
    this.postService.addPost(this.postData).subscribe(newPost => {
      this.posts.push(newPost);
      this.posts.sort((a, b) => (a.code.toLowerCase() > b.code.toLowerCase()) ? 1 : -1);
      this.resetFormValues();
      this.onPostUpdated.emit("");
    });
  }

  deletePost(id: number): void {
    this.postService.deletePost(id).subscribe(() => {
      this.posts = this.posts.filter(x => x.id != id);
      this.onPostUpdated.emit("");
    });
  }

  loadValuesToForm(post: Post): void {
    this.postId = post.id;
    this.town = post.town;
    this.capacity = post.capacity;
    this.code = post.code;
    this.update = true;
  }

  updatePost(): void {
    this.postData.id = this.postId;
    this.postData.town = this.town;
    this.postData.capacity = this.capacity;
    this.postData.code = this.code;
    this.postService.updatePost(this.postData).subscribe(updatedPost => {
      let index = this.posts.map(e => e.id).indexOf(updatedPost.id);
      this.posts[index] = updatedPost;
      this.posts.sort((a, b) => (a.code.toLowerCase() > b.code.toLowerCase()) ? 1 : -1);
      this.resetFormValues();
      this.onPostUpdated.emit("");
    });
  }

  resetFormValues(): void {
    this.postId = null as any;
    this.town = '';
    this.capacity = null as any;
    this.code = '';
    this.update = false;
  }

}