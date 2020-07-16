<template>
  <div>
    <v-dialog v-model="show" max-width="800" persistent>
      <v-form @submit.prevent="save">
        <v-card class="pa-5">
          <v-card-title>
            {{ modalTitle }}
            <div v-show="hasInternalError">
              {{ internalErrorMsg }}
            </div>
          </v-card-title>
          <v-card-text>
            <v-row dense>
              <v-col cols="6">
                <v-text-field
                  v-model="item.customerName"
                  label="Customer Name"
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-text-field
                  v-model="item.location"
                  label="Location"
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-text-field
                  v-model="item.contactNum"
                  label="Contact Number"
                ></v-text-field>
              </v-col>
            </v-row>
          </v-card-text>
          <v-card-actions fixed>
            <v-spacer></v-spacer>
            <ButtonSave type="submit" class="mr-2" />
            <ButtonCancel @click="cancel()" />
          </v-card-actions>
        </v-card>
      </v-form>
    </v-dialog>
  </div>
</template>

<script>
import notiMixin from "@/mixins/notiMixin.js";
import customerService from "@/services/customerService.js";

export default {
  name: "Customer",
  mixins: [notiMixin],
  props: {
    id: {
      type: Number,
      default: 0
    }
  },
  data() {
    return {
      modalTitle: "",
      show: true,
      item: {},
      internalErrorMsg: ""
    };
  },
  computed: {
    hasInternalError() {
      return !!this.internalErrorMsg;
    }
  },
  watch: {
    show(value) {
      if (!value) {
        this.$router.back();
      }
    }
  },
  created() {
    if (this.id == 0) {
      this.item = this.defaultItem();
      this.modalTitle = "Add New Customer";
    } else {
      this.item = this.getDetails();
      this.modalTitle = "Update Customer";
    }
  },
  beforeCreate() {},
  methods: {
    defaultItem() {
      return {
        id: 0,
        customerName: "",
        location: "",
        contactNum: ""
      };
    },
    save() {
      if (this.id == 0) {
        return this.create();
      } else {
        return this.update();
      }
    },
    async create() {
      const noti = {
        visible: true
      };

      try {
        await customerService.create(this.item);
        noti.type = "success";
        noti.content = "New customer added successfully";
        this.close();
      } catch (error) {
        noti.type = "error";
        this.internalErrorMsg = error;
        noti.content = error;
      } finally {
        this.showNotification(noti);
      }
    },
    async update() {
      const noti = {
        visible: true
      };

      try {
        await customerService.update(this.item.id, this.item);
        noti.type = "success";
        noti.content = "Customer Updated successfully";
        this.close();
      } catch (error) {
        noti.type = "error";
        noti.content = error;
        this.internalErrorMsg = error;
      } finally {
        this.showNotification(noti);
      }
    },
    cancel() {
      this.close();
    },
    close() {
      this.$router.back();
    },
    async getDetails() {
      try {
        const response = await customerService.getById(this.id);
        this.item = response.data;
      } catch (error) {
        console.log(error);
      }
    }
  }
};
</script>

<style></style>
