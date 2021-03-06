<template>
  <div>
    <v-dialog v-model="show" max-width="700" persistent>
      <ValidationObserver ref="form" tag="form" @submit.prevent="save">
        <v-card class="pa-5">
          <v-card-title>
            <span>{{ modalTitle }} </span>
            <div v-show="hasInternalError">
              {{ internalErrorMsg }}
            </div>
          </v-card-title>
          <v-card-text>
            <v-row dense>
              <v-col cols="6">
                <ValidationProvider
                  v-slot="{ errors }"
                  mode="lazy"
                  name="Code"
                  :rules="{
                    required: true,
                    max: 50,
                    remote: { url: '/shift/validatecode', obj: item }
                  }"
                >
                  <v-text-field
                    v-model="item.ShiftCode"
                    :error-messages="errors"
                    label="Code"
                  ></v-text-field>
                </ValidationProvider>
              </v-col>
              <v-col cols="6">
                <ValidationProvider
                  v-slot="{ errors }"
                  mode="lazy"
                  name="Description"
                  :rules="{
                    required: true,
                    max: 50,
                    remote: { url: '/shift/validatedesc', obj: item }
                  }"
                >
                  <v-text-field
                    v-model="item.ShiftDesc"
                    :error-messages="errors"
                    :scrollable="true"
                    label="Description"
                  ></v-text-field>
                </ValidationProvider>
              </v-col>
              <v-col cols="6">
                <h3>Start:</h3>
                <v-time-picker
                  v-model="item.TimeFrom"
                  :scrollable="true"
                  width="200"
                ></v-time-picker>
              </v-col>
              <v-col cols="6">
                <h3>End:</h3>
                <v-time-picker
                  v-model="item.TimeTo"
                  :scrollable="true"
                  width="200"
                ></v-time-picker>
              </v-col>
            </v-row>
          </v-card-text>
          <v-card-actions fixed>
            <v-spacer></v-spacer>
            <ButtonSave type="submit" class="mr-2" />
            <ButtonCancel @click="cancel()" />
          </v-card-actions>
        </v-card>
      </ValidationObserver>
    </v-dialog>
  </div>
</template>

<script>
import notiMixin from "@/mixins/notiMixin.js";
import shiftService from "@/services/shiftService.js";
import "@/validations/veeValidateExtensions.js";
import { ValidationProvider, ValidationObserver } from "vee-validate";

export default {
  name: "Shifts",
  components: {
    ValidationProvider,
    ValidationObserver
  },
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
        ShiftId: 0,
        ShiftCode: "",
        ShiftDesc: "",
        TimeFrom: "",
        TimeTo: ""
      };
    },
    save() {
      this.$refs.form.validate().then(success => {
        if (!success) {
          return;
        }
        if (this.id == 0) {
          this.cancelled = false;
          return this.create();
        } else {
          return this.update();
        }
      });
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
        await shiftService.update(this.item.ShiftId, this.item);
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
