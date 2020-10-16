<template>
  <solar-modal>
    <template v-slot:header>
      Add New Customer
    </template>
    <template v-slot:body>
      <ul class="new-customer">
        <li>
          <label for="firstName">First Name</label>
          <input id="firstName" type="text" v-model="customer.firstName" />
        </li>
        <li>
          <label for="lastName">Last Name</label>
          <input id="lastName" type="text" v-model="customer.lastName" />
        </li>
        <li>
          <label for="address">Address Line</label>
          <input
            id="address"
            type="text"
            v-model="customer.primaryAddress.addressLine"
          />
        </li>
        <li>
          <label for="address2">Address Line 2</label>
          <input
            id="address2"
            type="text"
            v-model="customer.primaryAddress.addressLine2"
          />
        </li>
        <li>
          <label for="city">City</label>
          <input id="city" type="text" v-model="customer.primaryAddress.city" />
        </li>
        <li>
          <label for="state">State</label>
          <input
            id="state"
            type="text"
            v-model="customer.primaryAddress.state"
          />
        </li>
        <li>
          <label for="postalCode">Postal Code</label>
          <input
            id="postalCode"
            type="text"
            v-model="customer.primaryAddress.postalCode"
          />
        </li>
      </ul>
    </template>
    <template v-slot:footer>
      <solar-button
        type="button"
        @button:click="save"
        aria-label="save new customer"
      >
        <solar-button
          type="button"
          @button:click="close"
          aria-label="Close modal"
        >
          Close
        </solar-button>
      </solar-button>
    </template>
  </solar-modal>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import SolarButton from '@/components/SolarButton.vue';
import SolarModal from '@/components/modals/SolarModal.vue';
import { ICustomer } from '@/types/Customer';

@Component({
  name: 'NewCustomerModal',
  components: { SolarButton, SolarModal },
})
export default class NewCustomerModal extends Vue {
  customer: ICustomer = {};

  save() {
    this.$emit('save:custoemr', this.customer);
  }

  close() {
    this.$emit('close');
  }
}
</script>

<style lang="scss" scoped>
.new-customer {
  list-style: none;
  padding: 0;
  margin: 0;

  display: flex;
  flex-wrap: wrap;

  input {
    width: 80%;
    height: 1.8rem;
    margin: 0.8rem 2rem 0.8rem 0.8rem;
    font-size: 1.1rem;
    line-height: 1.3rem;
    padding: 0.2rem;
    color: #555;
  }

  label {
    font-weight: bold;
    margin: 0.8rem;
    display: block;
  }
}
</style>
