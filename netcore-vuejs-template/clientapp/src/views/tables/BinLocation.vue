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
                  name="Description"
                  :rules="{
                    required: true,
                    max: 50,
                    remote: { url: '/binlocation/validatedesc', obj: item }
                  }"
                >
                  <v-text-field
                    v-model="item.BinLocDesc"
                    :error-messages="errors"
                    class="value-uppercase "
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
import { mapState, mapGetters, mapActions } from "vuex";
import "@/validations/veeValidateExtensions.js";
import { ValidationProvider, ValidationObserver } from "vee-validate";

export default {
  name: "BinLocation",
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
      internalErrorMsg: ""
    };
  },
  computed: {
    ...mapGetters("binLocation", ["defaultItem"]),
    ...mapState("binLocation", ["item"])
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
      this.id == 0 ? "Add New Bin Location" : "Update Bin Location";
    await this.get(this.id);
  },
  methods: {
    ...mapActions("binLocation", {
      get: "getBinLocation",
      create: "createBinLocation",
      update: "updateBinLocation"
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
