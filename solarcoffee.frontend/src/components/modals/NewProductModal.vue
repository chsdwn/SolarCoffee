<template>
  <solar-modal>
    <template v-slot:header>
      Add New Product
    </template>

    <template v-slot:body>
      <ul class="newProduct">
        <li>
          <label for="productName">Name</label>
          <input id="productName" type="text" v-model="newProduct.name" />
        </li>
        <li>
          <label for="productDescription">Description</label>
          <input
            id="productDescription"
            type="text"
            v-model="newProduct.description"
          />
        </li>
        <li>
          <label for="productPrice">Price</label>
          <input id="productPrice" type="number" v-model="newProduct.price" />
        </li>
        <li>
          <label for="isTaxable">Is this product taxable?</label>
          <input
            id="isTaxable"
            type="checkbox"
            v-model="newProduct.isTaxable"
          />
        </li>
      </ul>
    </template>

    <template v-slot:footer>
      <solar-button
        type="button"
        @click.native="save"
        aria-label="save new item"
      >
        Save Product
      </solar-button>
      <solar-button type="button" @click.native="close" aria-label="close">
        Close
      </solar-button>
    </template>
  </solar-modal>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

import SolarButton from '@/components/SolarButton.vue';
import SolarModal from '@/components/modals/SolarModal.vue';

import { IProduct } from '@/types/Product';

@Component({
  name: 'NewProductModal',
  components: { SolarButton, SolarModal },
})
export default class NewProductModal extends Vue {
  newProduct: IProduct = {
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
    this.$emit('save:product', this.newProduct);
  }

  close() {
    this.$emit('close');
  }
}
</script>

<style lang="scss" scoped>
.newProduct {
  list-style: none;
  padding: 0;
  margin: 0;

  input {
    width: 100%;
    height: 1.8rem;
    margin-bottom: 1.2rem;
    font-size: 1.1rem;
    line-height: 1.3rem;
    padding: 0.2rem;
    color: #555;
  }

  label {
    font-weight: bold;
    margin-bottom: 0.3rem;
    display: block;
  }
}
</style>
