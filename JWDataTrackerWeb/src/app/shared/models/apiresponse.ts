export class ApiResponse<T> {
    constructor() {
        this.isSuccess = false;
        this.message = "";
        this.data = null;
        this.hasException = false;
    }
    isSuccess: boolean;
    message: string;
    data: T;
    hasException: boolean;
}