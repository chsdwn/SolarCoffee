import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';

import CreateInvoice from '@/views/CreateInvoice.vue';
import Customers from '@/views/Customers.vue';
import Inventory from '@/views/Inventory.vue';
import Orders from '@/views/Orders.vue';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'home',
    component: Inventory,
  },
  {
    path: '/customers',
    name: 'customers',
    component: Customers,
  },
  {
    path: '/inventory',
    name: 'inventory',
    component: Inventory,
  },
  {
    path: '/invoice/new',
    name: 'create-invoice',
    component: CreateInvoice,
  },
  {
    path: '/orders',
    name: 'orders',
    component: Orders,
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
