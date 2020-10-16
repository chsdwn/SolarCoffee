export interface IServiceResponse<T> {
  data: T;
  message: string;
  isSuccess: boolean;
  time: Date;
}
