<template>
  <div class="inventory-container">
    <h1 id="inventoryTitle">Inventory Dashboard</h1>
    <hr />
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
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { IProductInventory } from '@/types/Product';

@Component({
  name: 'Inventory',
  components: {},
})
export default class Inventory extends Vue {
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
}
</script>

<style lang="scss" scoped></style>
