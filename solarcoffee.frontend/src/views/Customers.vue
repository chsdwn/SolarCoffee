<template>
  <div>
    <h1 id="customersTitle">Manage Customers</h1>
    <hr />
    <div class="customer-actions">
      <solar-button @button:click="showNewCustomerModal">
        Add Customer
      </solar-button>
    </div>
    <table id="customers" class="table">
      <tr>
        <th>Customer</th>
        <th>Address</th>
        <th>State</th>
        <th>Since</th>
        <th>Delete</th>
      </tr>

      <tr v-for="customer in customers" v-bind:key="customer.id">
        <td>{{ `${customer.firstName} ${customer.lastName}` }}</td>
        <td>
          {{
            `${customer.primaryAddress.addressLine} ${customer.primaryAddress.addressLine2}`
          }}
        </td>
        <td>{{ customer.primaryAddress.state }}</td>
        <td>{{ customer.createdOn | humanizeDate }}</td>
        <td>
          <i
            class="lni lni-cross-circle customer-delete"
            @click="deleteCustomer(customer.id)"
          ></i>
        </td>
      </tr>
    </table>

    <new-customer-modal
      @save:customer="saveNewCustomer"
      @close="closeModal"
      v-if="isCustomerModalVisible"
    />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import NewCustomerModal from '@/components/modals/NewCustomerModal.vue';
import SolarButton from '@/components/SolarButton.vue';

import { CustomerService } from '@/services/customer-service';

import { ICustomer } from '@/types/Customer';

const customerService = new CustomerService();

@Component({
  name: 'Customers',
  components: { NewCustomerModal, SolarButton },
})
export default class Customers extends Vue {
  customers: ICustomer[] = [];
  isCustomerModalVisible = false;

  async created() {
    await this.initialize();
  }

  async initialize() {
    this.customers = await customerService.getCustomers();
  }

  showNewCustomerModal() {
    this.isCustomerModalVisible = true;
  }

  async deleteCustomer(id: number) {
    await customerService.deleteCustomer(id);
    await this.initialize();
  }

  async saveNewCustomer(customer: ICustomer) {
    await customerService.addCustomer(customer);
    this.isCustomerModalVisible = false;
    await this.initialize();
  }

  closeModal() {
    this.isCustomerModalVisible = false;
  }
}
</script>

<style lang="scss" scoped>
@import '@/scss/global.scss';

.customer-actions {
  margin-bottom: 0.8rem;
  display: flex;

  div {
    margin-right: 0.8rem;
  }
}

.customer-delete {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $solar-red;
}
</style>
