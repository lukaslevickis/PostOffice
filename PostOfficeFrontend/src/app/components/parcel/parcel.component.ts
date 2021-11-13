import { Component, OnInit } from '@angular/core';
import { Parcel } from 'src/app/models/parcel';
import { ParcelService } from 'src/app/services/parcel.service';
import { FormBuilder } from '@angular/forms';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';

@Component({
  selector: 'app-parcel',
  templateUrl: './parcel.component.html',
  styleUrls: ['./parcel.component.css']
})
export class ParcelComponent implements OnInit {
  private parcelService: ParcelService;
  private postService: PostService;
  public parcels: Parcel[] = [];
  public posts: Post[] = [];
  public postsWithCapacity: Post[] = [];
  update: boolean = false;
  postId2: number = 0;
  parcelId: number = null as any;
  weight: number = null as any;
  phone: string = '';
  info: string = '';
  postId: number = null as any;

  postData = {} as Parcel;
  parcelForm = this.formBuilder.group({
    weight: '',
    phone: null,
    info: '',
    postId: null,
  });

  constructor(parcelService: ParcelService, 
              postService: PostService, 
              private formBuilder: FormBuilder) { 
    this.parcelService = parcelService;
    this.postService = postService;
   }

   async ngOnInit(): Promise<void> {
    await this.getParcels();
    await this.getPosts();
  }

  addParcel(): void {
    this.postData.id = this.parcelForm.value.id;
    this.postData.weight = this.parcelForm.value.weight;
    this.postData.phone = this.parcelForm.value.phone;
    this.postData.info = this.parcelForm.value.info;
    this.postData.postId = this.parcelForm.value.postId;
    this.parcelService.addParcel(this.postData).subscribe(newParcel => {
      this.parcels.push(newParcel);
      this.parcels.sort((a, b) => (a.weight < b.weight) ? 1 : -1);
      this.resetFormValues();
      this.getPostsWithCapacity();
    });
  }

  deleteParcel(id: number): void {
    this.parcelService.deleteParcel(id).subscribe(() => {
      this.parcels = this.parcels.filter(x => x.id != id);
      this.getPostsWithCapacity();
    });
  }

  public async getParcels(): Promise<void> {
    this.parcels = await this.parcelService.getParcels().toPromise();
  }

  public async getPosts(): Promise<void> {
    this.posts = await this.postService.getPosts().toPromise();
    this.getPostsWithCapacity();
  }

  getPostsWithCapacity(): void {
    this.postsWithCapacity = [];
    this.posts.forEach(post => {
      let capacity = 0;
      this.parcels.forEach(parcel => {
        if(parcel.postId == post.id) {
          capacity++;
        }
      });

      if(post.capacity > capacity) {
        this.postsWithCapacity.push(post);
      }
    });
  }

  postUpdated(value: Event): void {
    this.getPosts();
  }

  loadValuesToForm(parcel: Parcel): void {
    this.postId = parcel.id;
    this.weight = parcel.weight;
    this.phone = parcel.phone;
    this.info = parcel.info;
    this.postId = parcel.postId;
    this.update = true;
  }

  updateParcel(): void {
    this.postData.id = this.postId;
    this.postData.weight = this.weight;
    this.postData.phone = this.phone;
    this.postData.info = this.info;
    this.postData.postId = this.postId;
    this.parcelService.updateParcel(this.postData).subscribe(updatedParcel => {
      let index = this.parcels.map(e => e.id).indexOf(updatedParcel.id);
      this.parcels[index] = updatedParcel;
      this.parcels.sort((a, b) => (a.weight < b.weight) ? 1 : -1);
      this.resetFormValues();
    });
  }

  resetFormValues(): void {
    this.parcelId = null as any;
    this.weight = null as any;
    this.phone = '';
    this.info = '';
    this.postId = null as any;
    this.update = false;
  }

  filterTable(id: number): void {
    if (id == 0) {
      this.getParcels();
    } else {
      this.parcelService.getPostParcels(id).subscribe((parcelsFromApi) => {
        this.parcels = parcelsFromApi;
      });
    }
  }

}