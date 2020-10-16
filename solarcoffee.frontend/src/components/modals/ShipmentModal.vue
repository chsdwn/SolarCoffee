<template>
  <solar-modal>
    <template v-slot:header>
      Receive Shipment
    </template>

    <template v-slot:body>
      <label for="product">Product Received: </label>
      <select id="product" class="shipmentItems" v-model="selectedProduct">
        <option value="" disabled>Please select one</option>
        <option v-for="item in inventory" :value="item" :key="item.product.id">
          {{ item.product.name }}
        </option>
      </select>
      <label for="qtyReceived">Quantity Received: </label>
      <input type="number" id="qtyReceived" v-model="qtyReceived" />
    </template>

    <template v-slot:footer>
      <solar-button
        type="button"
        @button:click="save"
        aria-label="save new shipment"
      >
        Save Received Shipment
      </solar-button>

      <solar-button
        type="button"
        @button:click="close"
        aria-label="Close modal"
      >
        Close
      </solar-button>
    </template>
  </solar-modal>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

import SolarButton from '@/components/SolarButton.vue';
import SolarModal from '@/components/modals/SolarModal.vue';

import { IProduct, IProductInventory } from '@/types/Product';
import { IShipment } from '@/types/Shipment';

@Component({
  name: 'ShipmentModal',
  components: { SolarButton, SolarModal },
})
export default class ShipmentModal extends Vue {
  @Prop({ required: true, type: Array as () => IProductInventory[] })
  inventory!: IProductInventory[];

  qtyReceived = 0;

  selectedProduct: IProduct = {
    id: 0,
    name: '',
    description: '',
    price: 0,
    isTaxable: false,
    isArchived: false,
    createdOn: new Date(),
    updatedOn: new Date(),
  };

  save() {
    const shipment: IShipment = {
      productId: this.selectedProduct.id,
      adjusment: this.qtyReceived,
    };

    this.$emit('save:shipment', shipment);
  }

  close() {
    this.$emit('close');
  }
}
</script>

<style></style>
