<template>
  <div>
    <v-dialog v-model="show" max-width="500" persistent>
      <ValidationObserver ref="form" @submit.prevent="save">
        <v-card class="pa-5">
          <v-card-title>
            <span>{{ modalTitle }}</span>
            <div v-show="hasInternalError">{{ internalErrorMsg }}</div>
          </v-card-title>
          <v-card-text>
            <v-row dense>
              <v-col cols="12">
                <ValidationProvider
                  v-slot="{ errors }"
                  mode="lazy"
                  name="Description"
                  :rules="{
                    required: true,
                    max: 50,
                    remote: { url: '/forklift/ValidateForkLiftNum', obj: item }
                  }"
                >
                  <v-text-field
                    v-model="item.ForkliftNum"
                    :error-messages="errors"
                    label="Forklift Number"
                  ></v-text-field>
                </ValidationProvider>
              </v-col>
              <v-col cols="12">
                <ValidationProvider
                  v-slot="{ errors }"
                  mode="lazy"
                  name="Updated Tare Wt."
                  :rules="{
                    between: { min: 1000, max: 9000 }
                  }"
                >
                  <v-text-field
                    v-model.number="item.UpdatedTareWt"
                    type="number"
                    :error-messages="errors"
                    label="Updated Tare Wt."
                  ></v-text-field>
                </ValidationProvider>
              </v-col>
            </v-row>
          </v-card-text>
          <v-card-actions fixed>
            <v-spacer></v-spacer>
            <ButtonSave type="submit" class="mr-2" @click="save" />
            <ButtonCancel @click="cancel()" />
          </v-card-actions>
        </v-card>
      </ValidationObserver>
    </v-dialog>
  </div>
</template>

<script>
import notiMixin from "@/mixins/notiMixin.js";
import forkliftService from "@/services/forkliftService.js";
import "@/validations/veeValidateExtensions.js";
import { ValidationProvider, ValidationObserver } from "vee-validate";

export default {
  name: "Forklift",
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
      this.modalTitle = "Add New Forklift";
    } else {
      this.item = this.getDetails();
      this.modalTitle = "Update Forklift";
    }
  },
  beforeCreate() {},
  methods: {
    defaultItem() {
      return {
        ForkliftId: 0,
        ForkliftNum: "",
        UpdatedTareWt: 0
      };
    },
    async save() {
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
        await forkliftService.create(this.item);
        noti.type = "success";
        noti.content = "New Forklift added successfully";
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
        await forkliftService.update(this.item.ForkliftId, this.item);
        noti.type = "success";
        noti.content = "Forklift updated successfully";
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
      this.cancelled = true;
      this.close();
    },
    close() {
      this.show = false;
    },
    async getDetails() {
      try {
        const response = await forkliftService.getById(this.id);
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
