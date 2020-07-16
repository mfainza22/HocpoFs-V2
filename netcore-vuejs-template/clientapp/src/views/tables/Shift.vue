<template>
  <div>
    <v-dialog v-model="show" max-width="500" persistent>
      <v-form @submit.prevent="save">
        <v-card class="pa-5">
          <v-card-title>
            <span>{{ modalTitle }} </span>
            <div v-show="hasInternalError">
              {{ internalErrorMsg }}
            </div>
          </v-card-title>
          <v-card-text>
            <v-row dense>
              <v-col cols="12">
                <v-text-field
                  v-model="item.ShiftCode"
                  label="Code"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  v-model="item.ShiftDesc"
                  label="Description"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  v-model="item.TimeFrom"
                  label="(Time) Start"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  v-model="item.TimeTo"
                  label="(Time) End"
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
import shiftService from "@/services/shiftService.js";

export default {
  name: "Shift",
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
      this.modalTitle = "Add New Shift";
    } else {
      this.item = this.getDetails();
      this.modalTitle = "Update Shift";
    }
  },
  methods: {
    defaultItem() {
      return {
        id: 0,
        ShiftCode: "",
        ShiftDesc: "",
        TimeFrom: "",
        TimeTo: ""
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
        await shiftService.create(this.item);
        noti.type = "success";
        noti.content = "New Shift added successfully";
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
        await shiftService.update(this.item.id, this.item);
        noti.type = "success";
        noti.content = "Shift updated successfully";
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
        const response = await shiftService.getById(this.id);
        console.log(response.data);
        this.item = response.data;
      } catch (error) {
        console.log(error);
      }
    }
  },
  beforeRouteLeave(to, from, next) {
    this.$route.meta.cancelled = this.cancelled;
    next();
  }
};
</script>

<style></style>
