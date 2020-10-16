<template>
  <div class="inventory-container">
    <h1 id="inventoryTitle">Inventory Dashboard</h1>
    <hr />

    <div class="inventory-actions">
      <solar-button id="addNewBtn" @button:click="showNewProductModal">
        Add New Item
      </solar-button>
      <solar-button id="receiveShipmentBtn" @button:click="showShipmentModal">
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
        <td
          v-bind:class="
            `${applyColor(item.quantityOnHand, item.idealQuantity)}`
          "
        >
          {{ item.quantityOnHand }}
        </td>
        <td>{{ item.product.price | price }}</td>
        <td>
          <span v-if="item.product.isTaxable">Yes</span>
          <span v-else>No</span>
        </td>
        <td>
          <i
            class="lni lni-cross-circle product-archive"
            @click="archiveProduct(item.product.id)"
          ></i>
        </td>
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
import { ProductService } from '@/services/product-service';

import { IProduct, IProductInventory } from '@/types/Product';
import { IShipment } from '@/types/Shipment';

const inventoryService = new InventoryService();
const productService = new ProductService();

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

  applyColor(current: number, target: number) {
    if (current <= 0) return 'red';
    if (Math.abs(target - current) > 10) return 'yellow';
    return 'green';
  }

  async archiveProduct(id: number) {
    await productService.archive(id);
    await this.initialize();
  }

  async saveNewProduct(newProduct: IProduct) {
    await productService.save(newProduct);
    this.isNewProductVisible = false;
    await this.initialize();
  }

  async saveNewShipment(shipment: IShipment) {
    await inventoryService.updateInventoryQuantity(shipment);
    this.isShipmentVisible = false;
    await this.initialize();
  }

  closeModals() {
    this.isNewProductVisible = false;
    this.isShipmentVisible = false;
  }

  async initialize() {
    this.inventory = await inventoryService.getInventory();
  }

  async created() {
    await this.initialize();
  }
}
</script>

<style lang="scss" scoped>
@import '@/scss/global.scss';

.green {
  font-weight: bold;
  color: $solar-green;
}

.yellow {
  font-weight: bold;
  color: $solar-yellow;
}

.red {
  font-weight: bold;
  color: $solar-red;
}

.inventory-actions {
  margin-bottom: 0.8rem;
  display: flex;
}

.product-archive {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $solar-red;
}
</style>
