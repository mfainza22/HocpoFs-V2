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
                  name="Code"
                  :rules="{
                    required: true,
                    max: 50,
                    remote: { url: '/warehouse/validatecode', obj: item }
                  }"
                >
                  <v-text-field
                    v-model="item.WarehouseCode"
                    :error-messages="errors"
                    label="Description"
                    class="value-uppercase"
                  ></v-text-field>
                </ValidationProvider>
                <ValidationProvider
                  v-slot="{ errors }"
                  mode="lazy"
                  name="Description"
                  :rules="{
                    required: true,
                    max: 50,
                    remote: { url: '/warehouse/validatename', obj: item }
                  }"
                >
                  <v-text-field
                    v-model="item.WarehouseName"
                    :error-messages="errors"
                    label="Description"
                    class="value-uppercase"
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
import { mapState, mapGetters, mapActions } from "vuex";
import "@/validations/veeValidateExtensions.js";
import { ValidationProvider, ValidationObserver } from "vee-validate";

export default {
  name: "Warehouse",
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
      show: true
    };
  },
  computed: {
    ...mapGetters("warehouse", ["defaultItem"]),
    ...mapState("warehouse", ["item"])
  },
  watch: {
    show(value) {
      if (!value) {
        this.$router.back();
      }
    }
  },
  async created() {
    this.modalTitle =
      this.id == 0 ? "Add New Warehouse" : "Update Warehouse Type";
    await this.get(this.id);
  },
  methods: {
    ...mapActions("warehouse", {
      get: "getWarehouse",
      create: "createWarehouse",
      update: "updateWarehouse"
    }),
    save() {
      this.$refs.form.validate().then(async success => {
        if (!success) {
          return;
        }
        if (this.id == 0) {
          this.cancelled = false;
          await this.create(this.item);
        } else {
          await this.update(this.item);
        }

        this.close();
      });
    },
    cancel() {
      this.close();
    },
    close() {
      this.$router.back();
    }
  },
  beforeRouteLeave(to, from, next) {
    this.$route.meta.cancelled = this.cancelled;
    next();
  }
};
</script>

<style></style>
