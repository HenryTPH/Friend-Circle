import { Photo } from "./photo"


// Section 103
export interface Member {
    id: number
    userName: string
    photoUrl: string
    age: number
    knownAs: string
    created: string
    lastActive: string
    gender: string
    introduction: string
    lookingFor: string
    interests: string
    city: string
    country: string
    photos: Photo[]
  }
  
