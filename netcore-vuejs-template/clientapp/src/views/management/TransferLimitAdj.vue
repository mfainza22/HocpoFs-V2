<template>
  <div>
    <v-dialog v-model="show" max-width="500" persistent>
      <ValidationObserver ref="form" tag="form" @submit.prevent="save">
        <v-card class="pa-5">
          <v-card-title>
            <span>{{ modalTitle }} </span>
          </v-card-title>
          <v-card-text>
            <v-row dense>
              <v-col cols="12">
                <ValidationProvider
                  v-slot="{ errors }"
                  mode="lazy"
                  name="Adjusted Limit"
                  :rules="{
                    required: true,
                    min_value: 1
                  }"
                >
                  <v-text-field
                    v-model.number="item.AdjLimit"
                    :error-messages="errors"
                    label="Specify adjustment in kilogram"
                    type="number"
                  ></v-text-field>
                </ValidationProvider>
              </v-col>
              <v-col cols="12">
                <ValidationProvider
                  v-slot="{ errors }"
                  mode="lazy"
                  name="Remarks"
                  :rules="{
                    required: true,
                    max: 1000
                  }"
                >
                  <v-textarea
                    v-model="item.AdjRemarks"
                    :error-messages="errors"
                    label="Remarks"
                  ></v-textarea>
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
import validationMixin from "@/mixins/validationMixin.js";
import transferLimitAdjService from "@/services/transferLimitAdjService.js";
import { mapActions } from "vuex";

export default {
  name: "TransferLimitAdj",
  mixins: [notiMixin, validationMixin],
  props: {
    id: {
      type: Number,
      default: 0
    },
    transferLimitId: {
      type: [Number, String],
      default: 0
    }
  },
  data() {
    return {
      modalTitle: "",
      show: true,
      item: {},
      cancelled: false
    };
  },
  created() {
    if (this.id == 0) {
      this.item = this.defaultItem();
      this.item.TransferLimitId = this.transferLimitId;
      this.modalTitle = "Add new adjustment";
    } else {
      this.item = this.getDetails();
      this.modalTitle = "Update adjustment";
    }
  },
  methods: {
    ...mapActions("transferLimit", ["listTransferLimit"]),
    defaultItem() {
      return {
        TransferLimitAdjId: 0,
        TransferLimitId: 0,
        AdjLimit: 0,
        AdjRemarks: "",
        AdjCreatedById: ""
      };
    },
    save() {
      try {
        this.$refs.form.validate().then(async success => {
          if (!success) {
            return;
          }
          if (this.id == 0) {
            this.cancelled = false;
            await this.create();
          } else {
            await this.update();
          }
        });
      } catch (error) {
        this.showNotification({
          type: "error",
          content: error
        });
      }
    },
    async create() {
      try {
        await transferLimitAdjService.create(this.item);
        await this.listTransferLimit();
        this.showNotification({
          type: "success",
          content: "New Adjustment was added"
        });
        this.close();
      } catch (error) {
        this.showNotification({
          type: "error",
          content: error
        });
      }
    },
    async update() {
      try {
        await transferLimitAdjService.update(
          this.item.TransferLimitAdjId,
          this.item
        );
        await this.listTransferLimit();
        this.showNotification({
          type: "success",
          content: "Selected adjustment updated successfully"
        });
        this.close();
      } catch (error) {
        this.showNotification({
          type: "error",
          content: error
        });
      }
    },
    cancel() {
      this.cancelled = true;
      this.close();
    },
    close() {
      this.$router.back();
    },
    async getDetails() {
      try {
        const response = await transferLimitAdjService.getById(this.id);
        this.item = response.data;
      } catch (error) {
        this.showNotification({
          type: "error",
          content: error
        });
        this.close();
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
