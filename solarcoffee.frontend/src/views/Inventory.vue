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

    <new-product-modal v-if="isNewProductVisible" @close="closeModals" />
    <shipment-modal
      v-if="isShipmentVisible"
      :inventory="inventory"
      @close="closeModals"
    />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import SolarButton from '@/components/SolarButton.vue';
import { IProductInventory } from '@/types/Product';

@Component({
  name: 'Inventory',
  components: { SolarButton },
})
export default class Inventory extends Vue {
  isNewProductVisible: boolean = false;
  isShipmentVisible: boolean = false;

  inventory: IProductInventory[] = [
    {
      id: 1,
      product: {
        id: 1,
        name: 'Product1',
        description: 'Description1',
        price: 100,
        isArchived: false,
        isTaxable: true,
        createdOn: new Date(),
        updatedOn: new Date(),
      },
      quantityOnHand: 1,
      idealQuantity: 10,
    },
    {
      id: 2,
      product: {
        id: 2,
        name: 'Product2',
        description: 'Description2',
        price: 200,
        isArchived: false,
        isTaxable: false,
        createdOn: new Date(),
        updatedOn: new Date(),
      },
      quantityOnHand: 2,
      idealQuantity: 20,
    },
  ];

  showNewProductModal() {}

  showShipmentModal() {}

  closeModals() {
    this.isNewProductVisible = false;
    this.isShipmentVisible = false;
  }
}
</script>

<style lang="scss" scoped></style>
