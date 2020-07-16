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
            <ButtonAdd :to="{ name: 'UnitCreate' }" />
            <v-text-field
              v-model="search"
              label="Search by Name"
              style="max-width:320px"
              color="grey"
              outlined
              dense
              hide-details
              prepend-icon="mdi-search"
              :show-select="true"
            ></v-text-field>
          </v-toolbar>
        </template>
        <template v-slot:item.id="{ item }">
          <v-btn
            fab
            x-small
            class="elevation-1"
            :to="{ name: 'UnitUpdate', params: { id: item.id } }"
          >
            <v-icon color="success">mdi-pencil</v-icon>
          </v-btn>
        </template>
      </v-data-table>
    </v-sheet>
    <router-view></router-view>
  </div>
</template>

<script>
import navbarMixins from "@/mixins/navbarMixins.js";
import notiMixin from "@/mixins/notiMixin.js";
import unitService from "@/services/unitService.js";

export default {
  mixins: [navbarMixins, notiMixin],
  data() {
    return {
      pageTitle: "Unit of Measurements",
      selected: [],
      search: "",
      items: [],
      tblHeaders: [
        {
          text: "Description",
          value: "unit",
          sortable: true,
          align: "left"
        },
        {
          text: "action",
          value: "id",
          align: "right"
        }
      ]
    };
  },
  async created() {
    try {
      const response = await unitService.list();
      this.items = response.data;
      console.log(this.$parent);
    } catch (error) {
      console.log(error);
    }
  },
  methods: {
    async remove() {
      if (this.selected.length == 0) return;

      const noti = {
        visible: true
      };

      try {
        let params = this.selected.map(a => a.id);
        await unitService.delete(params);
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
