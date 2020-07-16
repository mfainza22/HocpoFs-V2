<template>
  <div class="pa-10">
    <v-sheet class="elevation-1">
      <v-data-table
        v-model="selected"
        item-key="id"
        :headers="tblHeaders"
        :items="items"
        :search="search"
        show-select
      >
        <template v-slot:top>
          <v-toolbar flat>
            <ButtonDelete @click="remove()" />
            <v-spacer></v-spacer>
            <ButtonAdd :to="{ name: 'ProductCreate' }" />
            <v-text-field
              v-model="search"
              label="Search by Description"
              style="max-width:320px"
              color="grey"
              outlined
              dense
              hide-details
              prepend-icon="mdi-search"
              item-text="description"
              item-value="description"
              :show-select="true"
            ></v-text-field>
          </v-toolbar>
        </template>
        <template v-slot:item.img="{ item }">
          <v-avatar tile class="ma-2">
            <v-img :src="`/products/${item.img}`"></v-img>
          </v-avatar>
        </template>
        <template v-slot:item.description="{ item }">
          <div v-text="item.description"></div>
          <div class="caption grey--text" v-text="item.category"></div>
        </template>
        <template v-slot:item.id="{ item }">
          <v-btn
            fab
            x-small
            class="elevation-1"
            :to="{ name: 'ProductUpdate', params: { id: item.id } }"
          >
            <v-icon color="success">mdi-pencil</v-icon></v-btn
          >
        </template>
      </v-data-table>
    </v-sheet>
    <router-view></router-view>
  </div>
</template>

<script>
import navbarMixins from "@/mixins/navbarMixins.js";
import notiMixin from "@/mixins/notiMixin.js";
import productService from "@/services/productService";

export default {
  name: "Products",
  mixins: [navbarMixins, notiMixin],
  data() {
    return {
      pageTitle: "Products",
      selected: [],
      items: [],
      search: "",
      tblHeaders: [
        {
          text: "",
          align: "start",
          sortable: false,
          value: "img",
          class: "font-weight-bold",
          width: "100px"
        },
        {
          text: "Description",
          align: "start",
          sortable: false,
          value: "description",
          class: "font-weight-bold"
        },
        {
          text: "Brand",
          align: "start",
          sortable: false,
          value: "brand",
          class: "font-weight-bold"
        },
        {
          text: "Actions",
          align: "end",
          value: "id",
          sortable: false
        }
      ]
    };
  },
  created() {
    productService.list().then(response => {
      this.items = response.data;
    });
  },
  mounted() {},
  methods: {
    async remove() {
      if (this.selected.length == 0) return;

      const noti = {
        visible: true
      };

      try {
        let params = this.selected.map(a => a.id);
        await productService.delete(params);
        noti.type = "success";
        noti.content = `${params.length} ${
          params.length == 1 ? "record" : "records"
        } deleted successfully`;
      } catch (error) {
        noti.type = "error";
        this.internalErrorMsg = error;
        noti.content = error;
      } finally {
        this.showNotification(noti);
      }
    }
  }
};
</script>

<style></style>
