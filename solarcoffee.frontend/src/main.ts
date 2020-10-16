import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import moment from 'moment';

Vue.config.productionTip = false;

Vue.filter('price', function(price: number) {
  if (isNaN(price)) return '-';

  return '€' + price.toFixed(2);
});

Vue.filter('humanizeDate', function(date: Date) {
  return moment(date).format('Do MMMM YYYY');
});

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app');
