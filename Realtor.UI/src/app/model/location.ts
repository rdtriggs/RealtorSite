
export class LocationModel {
  Id: string;
  MlsNumber: string;
  Street1: string;
  Street2: string;
  City: string;
  State: string;
  ZipCode: string;
  Neighborhood: string;
  SalesPrice: string;
  DateListed: Date;
  Bedrooms: number;
  Bathrooms: number;
  GarageSize?: number;
  SquareFeet: number;
  LotSize?: number;
  Description: string;
  PhotoUrl: string;
  IsActive: boolean;
}

export class LocationList {
  LocationList: Array<Location> = new Array<Location>();
}
