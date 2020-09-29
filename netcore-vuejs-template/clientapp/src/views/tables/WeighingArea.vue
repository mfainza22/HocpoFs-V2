<template>
  <div>
    <v-dialog v-model="show" max-width="500" persistent>
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
              <v-col cols="12">
                <ValidationProvider
                  v-slot="{ errors }"
                  mode="lazy"
                  name="Code"
                  :rules="{
                    required: true,
                    max: 50,
                    remote: { url: '/weighingarea/validatecode', obj: item }
                  }"
                >
                  <v-text-field
                    v-model="item.AreaCode"
                    :error-messages="errors"
                    label="Code"
                  ></v-text-field>
                </ValidationProvider>
                <ValidationProvider
                  v-slot="{ errors }"
                  mode="lazy"
                  name="Description"
                  :rules="{
                    required: true,
                    max: 50,
                    remote: { url: '/weighingarea/validatedesc', obj: item }
                  }"
                >
                  <v-text-field
                    v-model="item.AreaDesc"
                    :error-messages="errors"
                    label="Description"
                  ></v-text-field>
                </ValidationProvider>
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
import weighingAreaService from "@/services/weighingAreaService.js";
import "@/validations/veeValidateExtensions.js";
import { ValidationProvider, ValidationObserver } from "vee-validate";

export default {
  name: "WeighingArea",
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
      this.modalTitle = "Add New weighing area";
    } else {
      this.item = this.getDetails();
      this.modalTitle = "Update weighing area";
    }
  },
  methods: {
    defaultItem() {
      return {
        WeighingAreaId: 0,
        AreaCode: "",
        AreaDesc: ""
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
        await weighingAreaService.create(this.item);
        noti.type = "success";
        noti.content = "New weighing area added successfully";
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
        await weighingAreaService.update(this.item.WeighingAreaId, this.item);
        noti.type = "success";
        noti.content = "Weighing area updated successfully";
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
        const response = await weighingAreaService.getById(this.id);
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
