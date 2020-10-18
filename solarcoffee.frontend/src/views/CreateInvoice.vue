<template>
  <div>
    <h1 id="invoiceTitle">Create Invoice</h1>
    <hr />
    <div class="invoice-step" v-if="invoiceStep === 1">
      <h2>Step 1: Select Customer</h2>
      <div class="invoice-step-detail">
        <label for="customer">Customer: </label>
        <select
          id="customer"
          class="invoice-customers"
          v-model="selectedCustomerId"
        >
          <option value="" disabled>Please select a customer</option>
          <option
            v-for="customer in customers"
            :value="customer.id"
            :key="customer.id"
          >
            {{ `${customer.firstName} ${customer.lastName}` }}
          </option>
        </select>
      </div>
    </div>

    <div class="invoice-step" v-if="invoiceStep === 2">
      <h2>Step 2: Create Order</h2>

      <div class="invoice-step-detail" v-if="inventory.length">
        <label for="product">Product: </label>
        <select id="product" class="invoiceLineItem" v-model="newItem.product">
          <option value="" disabled>Please select a Product</option>
          <option
            v-for="item in inventory"
            :value="item.product"
            :key="item.product.id"
          >
            {{ item.product.name }}
          </option>
        </select>
        <label for="quantity">Quantity: </label>
        <input id="quantity" type="number" min="0" v-model="newItem.quantity" />
      </div>

      <div class="invoice-item-actions">
        <solar-button :disabled="!canAddItem" @button:click="addLineItem">
          Add Line Item
        </solar-button>
        <solar-button
          :disabled="!lineItems.length"
          @button:click="finalizeOrder"
        >
          Finalize Order
        </solar-button>
      </div>

      <div class="invoice-order-list" v-if="lineItems.length">
        <div class="running-total">
          <h3>Running Total:</h3>
          {{ runningTotal | price }}
        </div>

        <hr />

        <table class="table" v-if="lineItems">
          <thead>
            <tr>
              <th>Product</th>
              <th>Description</th>
              <th>Qty</th>
              <th>Price</th>
              <th>Subtotal</th>
            </tr>
          </thead>
          <tr v-for="lineItem in lineItems" :key="lineItem.product.id">
            <td>{{ lineItem.product.name }}</td>
            <td>{{ lineItem.product.description }}</td>
            <td>{{ lineItem.quantity }}</td>
            <td>{{ lineItem.product.price }}</td>
            <td>{{ (lineItem.product.price * lineItem.quantity) | price }}</td>
          </tr>
        </table>
      </div>
    </div>

    <div class="invoice-step" v-if="invoiceStep === 3"></div>

    <hr />

    <div class="invoice-steps-actions">
      <solar-button @button:click="prev" :disabled="!canGoPrev">
        Previous
      </solar-button>
      <solar-button @button:click="next" :disabled="!canGoNext">
        Next
      </solar-button>
      <solar-button @button:click="startOver">Start Over</solar-button>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import SolarButton from '@/components/SolarButton.vue';

import { CustomerService } from '@/services/customer-service';
import { InventoryService } from '@/services/inventory-service';

import { ICustomer } from '@/types/Customer';
import { IInvoice, ILineItem } from '@/types/Invoice';
import { IProductInventory } from '@/types/Product';

const customerService = new CustomerService();
const inventoryService = new InventoryService();

@Component({
  name: 'CreateInvoice',
  components: { SolarButton },
})
export default class CreateInvoice extends Vue {
  invoiceStep = 1;
  invoice: IInvoice = {
    customerId: 0,
    lineItems: [],
    createdOn: new Date(),
    updatedOn: new Date(),
  };
  customers: ICustomer[] = [];
  selectedCustomerId = 0;
  inventory: IProductInventory[] = [];
  lineItems: ILineItem[] = [];
  newItem: ILineItem = { product: undefined, quantity: 0 };

  get canAddItem() {
    return this.newItem.product && this.newItem.quantity > 0;
  }

  get canGoNext() {
    return this.invoiceStep !== 3 && this.selectedCustomerId !== 0;
  }

  get canGoPrev() {
    return this.invoiceStep !== 1;
  }

  get runningTotal() {
    return this.lineItems.reduce(
      (prev, curr) => prev + curr?.product?.price * curr.quantity,
      0,
    );
  }

  async created() {
    await this.initialize();
  }

  async initialize() {
    this.customers = await customerService.getCustomers();
    this.inventory = await inventoryService.getInventory();
  }

  addLineItem() {
    const newItem: ILineItem = {
      product: this.newItem.product,
      quantity: Number(this.newItem.quantity),
    };

    const existingItemsId = this.lineItems.map(item => item?.product?.id);

    if (existingItemsId.includes(newItem?.product?.id)) {
      const lineItem = this.lineItems.find(
        item => item?.product?.id === newItem?.product?.id,
      );

      lineItem.quantity = +lineItem.quantity + newItem.quantity;
    } else {
      this.lineItems.push(this.newItem);
    }

    this.newItem = { product: undefined, quantity: 0 };
  }

  finalizeOrder() {
    this.invoiceStep = 3;
  }

  prev() {
    if (this.invoiceStep === 1) return;
    --this.invoiceStep;
  }

  next() {
    if (this.invoiceStep === 3) return;
    ++this.invoiceStep;
  }

  startOver() {
    this.invoice = { customerId: 0, lineItems: [] };
    this.invoiceStep = 1;
  }
}
</script>

<style lang="scss" scoped>
@import '@/scss/global.scss';

.invoice-steps-actions {
  width: 100%;
  display: flex;
}

.invoice-step {
}
.invoice-step-detail {
  margin: 1.2rem;
}

.invoice-order-list {
  margin-top: 1.2rem;
  padding: 0.8rem;

  .line-item {
    border-bottom: 1px dashed #ccc;
    padding: 0.8rem;

    display: flex;
  }

  .item-col {
    flex-grow: 1;
  }
}

.invoice-item-actions {
  display: flex;
}

.price-pre-tax {
  font-weight: bold;
}

.price-final {
  font-weight: bold;
  color: $solar-green;
}
</style>
