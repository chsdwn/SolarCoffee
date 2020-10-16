export interface ICustomer {
  id: number;
  firstName: string;
  lastName: string;
  primaryAddress: ICustomerAddress;
  createdOn: Date;
  updatedOn?: Date;
}

export interface ICustomerAddress {
  id: number;
  addressLine: string;
  addressLine2: string;
  city: string;
  state: string;
  postalCode: string;
  country: string;
  createdOn: Date;
  updatedOn?: Date;
}
