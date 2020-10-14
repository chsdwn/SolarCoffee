<template>
  <div class="inventory-container">
    <h1 id="inventoryTitle">Inventory Dashboard</h1>
    <hr />

    <div class="inventory-actions">
      <solar-button id="addNewBtn" @click.native="showNewProductModal">
        Add New Item
      </solar-button>
      <solar-button id="receiveShipmentBtn" @click.native="showShipmentModal">
        Receive Shipment
      </solar-button>
    </div>

    <table id="inventoryTable" class="table">
      <tr>
        <th>Item</th>
        <th>Quantity On-Hand</th>
        <th>Unit Price</th>
        <th>Taxable</th>
        <th>Delete</th>
      </tr>

      <tr v-for="item in inventory" :key="item.id">
        <td>{{ item.product.name }}</td>
        <td>{{ item.quantityOnHand }}</td>
        <td>{{ item.product.price | price }}</td>
        <td>
          <span v-if="item.product.isTaxable">Yes</span>
          <span v-else>No</span>
        </td>
        <td><div>X</div></td>
      </tr>
    </table>

    <new-product-modal
      v-if="isNewProductVisible"
      @save:product="saveNewProduct"
      @close="closeModals"
    />
    <shipment-modal
      v-if="isShipmentVisible"
      :inventory="inventory"
      @save:shipment="saveNewShipment"
      @close="closeModals"
    />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import NewProductModal from '@/components/modals/NewProductModal.vue';
import ShipmentModal from '@/components/modals/ShipmentModal.vue';
import SolarButton from '@/components/SolarButton.vue';

import { InventoryService } from '@/services/inventory-service';

import { IProduct, IProductInventory } from '@/types/Product';
import { IShipment } from '@/types/Shipment';

const inventoryService = new InventoryService();

@Component({
  name: 'Inventory',
  components: { NewProductModal, ShipmentModal, SolarButton },
})
export default class Inventory extends Vue {
  isNewProductVisible = false;
  isShipmentVisible = false;

  inventory: IProductInventory[] = [];

  showNewProductModal() {
    this.isNewProductVisible = true;
  }

  showShipmentModal() {
    this.isShipmentVisible = true;
  }

  saveNewProduct(newProduct: IProduct) {
    console.log(newProduct);
  }

  saveNewShipment(shipment: IShipment) {
    console.log(shipment);
  }

  closeModals() {
    this.isNewProductVisible = false;
    this.isShipmentVisible = false;
  }

  async fetchData() {
    this.inventory = await inventoryService.getInventory();
  }

  async created() {
    await this.fetchData();
  }
}
</script>

<style lang="scss" scoped></style>
