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
          class="invoiceCustomers"
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
    <div class="invoice-step" v-if="invoiceStep === 2"></div>
    <div class="invoice-step" v-if="invoiceStep === 3"></div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import { CustomerService } from '@/services/customer-service';
import { InventoryService } from '@/services/inventory-service';

import { ICustomer } from '@/types/Customer';
import { IInvoice, ILineItem } from '@/types/Invoice';
import { IProductInventory } from '@/types/Product';

const customerService = new CustomerService();
const inventoryService = new InventoryService();

@Component({
  name: 'CreateInvoice',
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

  async created() {
    await this.initialize();
  }

  async initialize() {
    this.customers = await customerService.getCustomers();
    this.inventory = await inventoryService.getInventory();
  }
}
</script>

<style lang="scss" scoped></style>
