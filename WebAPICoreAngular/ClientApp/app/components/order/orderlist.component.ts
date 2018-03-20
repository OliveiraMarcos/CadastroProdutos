import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';

@Component({
    selector: 'orderlist',
    templateUrl: './orderlist.component.html'
})
export class OrderListComponent {
    public orders: IOrder[];
    public order: Order;
    public customers: ICustomer[];
    public products: IProduct[];
    public formIsActive: boolean;

    public vDate: string[] = [];
    public vCust: string[] = [];
    public vObsv: string[] = [];
    public vProd: string[] = [];
    public vQtd: string[] = [];
    public vPric: string[] = [];

    private _http: Http;
    private _baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this._http = http;
        this._baseUrl = baseUrl;
        this.GetAllOrders();

    }

    public SalvaDados() {
        let header = new Headers()
        header.append('Content-Type', 'application/json');
        if (this.order.id) {
            this._http.put(this._baseUrl + 'api/Order/' + this.order.id.toString(), JSON.stringify(this.order), { headers: header }).subscribe(result => {
                this.OnSuccessSaveOrder(result.json() as IResponse);
            }, error => this.OnErrorSaveOrder(error));
        } else {
            this._http.post(this._baseUrl + 'api/Order', JSON.stringify(this.order), { headers: header }).subscribe(result => {
                this.OnSuccessSaveOrder(result.json() as IResponse);
            }, error => this.OnErrorSaveOrder(error));
        }
    }

    public OnErrorSaveOrder(error:any) {
        if (error._body) {
            let resp = JSON.parse(error._body) as IResponse;
            if (resp.statusCode == 422) {
                for (let ras of resp.rason) {
                    switch (ras.propertyName) {
                        case 'DeliveryDate':
                            this.vDate.push(ras.errorMessage);
                            break;
                        case 'Quantity':
                            this.vQtd.push(ras.errorMessage);
                            break;
                        case 'UnitPrice':
                            this.vPric.push(ras.errorMessage);
                            break;
                        case 'ProductId':
                            this.vProd.push(ras.errorMessage);
                            break;
                        case 'Observation':
                            this.vObsv.push(ras.errorMessage);
                            break;
                        case 'CustomerId':
                            this.vCust.push(ras.errorMessage);
                            break;
                    }
                }
            }
        }
    }

    public OnSuccessSaveOrder(response: IResponse) {
        this.GetAllOrders();
    }

    public RemoveOrder(id: number) {
        this._http.delete(this._baseUrl + 'api/Order/' + id.toString()).subscribe(result => {
            this.OnRemoveSuccess(result.json() as IResponse);
        }, error => console.error(error));
    }

    public OnRemoveSuccess(res: IResponse) {
        this.GetAllOrders();
    }

    public GetAllOrders() {
        this.formIsActive = false;
        this.vDate = [];
        this.vCust = [];
        this.vObsv = [];
        this.vProd = [];
        this.vQtd = [];
        this.vPric = [];
        this._http.get(this._baseUrl + 'api/Order').subscribe(result => {
            this.OnSuccessGetAllOrder(result.json() as IResponse);
        }, error => console.error(error));
    }

    public GetAllProduct() {
        this._http.get(this._baseUrl + 'api/Product').subscribe(result => {
            this.OnSuccessGetAllProduct(result.json() as IResponse);
        }, error => console.error(error));
    }

    public GetAllCustomer() {
        this._http.get(this._baseUrl + 'api/Customer').subscribe(result => {
            this.OnSuccessGetAllCustomer(result.json() as IResponse);
        }, error => console.error(error));
    }

    public GetOneOrder(id: number) {
        this._http.get(this._baseUrl + 'api/Order/' + id.toString()).subscribe(result => {
            this.OnSuccessGetOneOrder(result.json() as IResponse);
        }, error => console.error(error));
    }

    public EditOrder(id: number) {
        this.order = new Order();
        this.GetOneOrder(id);
        this.GetAllProduct();
        this.GetAllCustomer();
        this.formIsActive = true;
    }

    public NewOrder() {
        this.order = new Order();
        this.formIsActive = true;
        this.order.orderItem = [];
        this.GetAllProduct();
        this.GetAllCustomer();
        //console.error(this.order);
    }

    public RemoveOrderItem(idx: number) {
        this.order.orderItem.splice(idx, 1);
    }

    public AddOrderItem() {
        this.order.orderItem.push(new OrderItem());
        //console.error(this.order);
    }

    OnSuccessGetOneOrder(response: IResponse) {
        if (!response.hasError) {
            if (response.result) {
                this.order = response.result as Order;
            } else {
                this.order = new Order();
                this.order.orderItem = [];
            }
        }
    }

    public OnSuccessGetAllOrder(response: IResponse) {
        if (!response.hasError) {
            this.orders = response.result as IOrder[];
        }
    }

    public OnSuccessGetAllProduct(response: IResponse) {
        if (!response.hasError) {
            this.products = response.result as IProduct[];
        }
    }

    public OnSuccessGetAllCustomer(response: IResponse) {
        if (!response.hasError) {
            this.customers = response.result as ICustomer[];
        }
    }

    
}

interface IProduct {
    id: number;
    name: string
}

interface ICustomer {
    id: number;
    name: string
}

interface IOrder {
    id: number;
    customerId: number;
    deliveryDate: Date;
    taxExempt: boolean,
    observation: string;
    customer: ICustomer;
    total: number;
}

class OrderItem {
    id: number;
    orderId: number;
    productId: number;
    quantity: number = 0;
    unitPrice: number = 0;
}

class Order implements IOrder {
    id: number;
    customerId: number;
    deliveryDate: Date;
    taxExempt: boolean;
    observation: string;
    customer: ICustomer;
    total: number = 0;
    orderItem: OrderItem[];
}

interface IReson {
    propertyName: string;
    errorMessage: string;
}

interface IResponse {
    statusCode: number;
    hasError: boolean;
    message: string;
    result: object;
    rason: IReson[];
}