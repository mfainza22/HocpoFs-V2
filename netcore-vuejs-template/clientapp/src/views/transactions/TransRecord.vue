<template>
  <v-sheet v-if="isLoaded" class="trans">
    <ValidationObserver ref="form" tag="form" @submit.prevent="saveTrans">
      <v-container>
        <v-row>
          <v-col cols="8">
            <div class="mb-4">
              <div class="d-flex" style="over-flow: hidden;">
                <div v-if="!confirmSave" :key="1">
                  <div class="d-flex">
                    <ButtonSave class="mr-2" @click="preSaveTrans()" />
                    <ButtonCancel @click="cancel()" />
                  </div>
                </div>
                <div v-else :key="2">
                  <div class="d-flex align align-center">
                    <span class="subtitle-1 mr-2">Press Yes to Proceed.</span>
                    <ButtonYes class="mr-2" type="submit"> </ButtonYes>
                    <ButtonNo @click="confirmSave = false" />
                  </div>
                </div>
              </div>
            </div>

            <v-row>
              <v-col cols="4">
                <ValidationProvider
                  v-slot="{ errors }"
                  vid="PalletNum"
                  mode="lazy"
                  name="Pallet Number"
                  :rules="{
                    invalid_char: true
                  }"
                >
                  <v-text-field
                    v-model="transRecord.PalletNum"
                    :error-messages="errors"
                    placeholder=" "
                    label="Pallet Number"
                    dense
                    class="value-uppercase "
                  ></v-text-field>
                </ValidationProvider>
              </v-col>
              <v-col cols="4">
                <ValidationProvider
                  v-slot="{ errors }"
                  vid="PackagingTypeId"
                  mode="lazy"
                  name="Packaging Type"
                  :rules="{
                    required: true,
                    min_value: 1
                  }"
                >
                  <AutocompletePackagingType
                    v-model="transRecord.PackagingTypeId"
                    :error-messages="errors"
                    dense
                    class="value-uppercase"
                  />
                </ValidationProvider>
              </v-col>
              <v-col cols="4">
                <ValidationProvider
                  v-slot="{ errors }"
                  vid="EmptyPackageWt"
                  mode="lazy"
                  name="Empty Package Wt."
                  :rules="{}"
                >
                  <v-text-field
                    v-model.number="transRecord.EmptyPackageWt"
                    :error-messages="errors"
                    placeholder=" "
                    type="number"
                    label="Empty Pack. Wt"
                    min="0"
                    max="99999"
                    step="any"
                    dense
                    class="value-uppercase"
                    suffix="Kg"
                  ></v-text-field>
                </ValidationProvider>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="4">
                <ValidationProvider
                  v-slot="{ errors }"
                  vid="RawMaterialId"
                  mode="lazy"
                  name="Raw Material"
                  :rules="{
                    min_value: 0
                  }"
                >
                  <AutocompleteMaterial
                    v-model="transRecord.RawMaterialId"
                    :error-messages="errors"
                    dense
                    clearable
                    class="value-uppercase"
                  />
                </ValidationProvider>
              </v-col>
              <v-col cols="2" offset="4">
                <ValidationProvider
                  v-slot="{ errors }"
                  vid="Quantity"
                  mode="lazy"
                  name="Quantity"
                  :rules="{}"
                >
                  <v-text-field
                    v-model.number="transRecord.Quantity"
                    :error-messages="errors"
                    min="0"
                    max="99999"
                    step="any"
                    placeholder=" "
                    type="number"
                    label="Quantity"
                    dense
                  ></v-text-field>
                </ValidationProvider>
              </v-col>
              <v-col cols="2">
                <ValidationProvider
                  v-slot="{ errors }"
                  vid="WtPerPackage"
                  mode="lazy"
                  name="Weight Per Pack"
                  :rules="{
                    invalid_char: true
                  }"
                >
                  <v-text-field
                    v-model.number="transRecord.WtPerPackage"
                    :error-messages="errors"
                    placeholder=" "
                    type="number"
                    min="0"
                    max="99999"
                    step="any"
                    label="Weight Per Pack"
                    dense
                    suffix="Kg"
                  ></v-text-field>
                </ValidationProvider>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="4">
                <ValidationProvider
                  v-slot="{ errors }"
                  vid="BinLocationId"
                  mode="lazy"
                  name="Bin Location"
                  :rules="{
                    required: true,
                    invalid_char: true
                  }"
                >
                  <AutocompleteBinLocation
                    v-model="transRecord.BinLocationId"
                    :error-messages="errors"
                    clearable
                    dense
                    class="value-uppercase"
                  />
                </ValidationProvider>
              </v-col>
              <v-col cols="2">
                <ValidationProvider
                  v-slot="{ errors }"
                  vid="BinNumber"
                  mode="lazy"
                  name="Bin Number"
                  :rules="{
                    invalid_char: true
                  }"
                >
                  <v-text-field
                    v-model="transRecord.BinNumber"
                    :error-messages="errors"
                    placeholder=" "
                    label="Bin Number"
                    dense
                    class="value-uppercase"
                  ></v-text-field>
                </ValidationProvider>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="4">
                <ValidationProvider
                  v-slot="{ errors }"
                  vid="WarehouseId"
                  mode="lazy"
                  name="Bin Number"
                  :rules="{
                    required: true,
                    invalid_char: true
                  }"
                >
                  <AutocompleteWarehouse
                    v-model="transRecord.WarehouseId"
                    :error-messages="errors"
                    clearable
                    class="value-uppercase"
                    dense
                  />
                </ValidationProvider>
              </v-col>
              <v-col cols="4">
                <ValidationProvider
                  v-slot="{ errors }"
                  vid="LocationId"
                  mode="lazy"
                  name="Location"
                  :rules="{
                    invalid_char: true
                  }"
                >
                  <AutocompleteLocation
                    v-model="transRecord.LocationId"
                    :error-messages="errors"
                    dense
                    clearable
                    class="value-uppercase"
                  /> </ValidationProvider
              ></v-col>
            </v-row>
            <v-row>
              <v-col cols="8">
                <ValidationProvider
                  v-slot="{ errors }"
                  vid="Remarks"
                  mode="lazy"
                  name="Bin Number"
                  :rules="{
                    invalid_char: true
                  }"
                >
                  <v-textarea
                    v-model="transRecord.Remarks"
                    :error-messages="errors"
                    placeholder=" "
                    label="Remarks"
                    rows="2"
                    no-resize
                    dense
                    class="value-uppercase"
                  ></v-textarea>
                </ValidationProvider>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="8">
                <div class="d-flex">
                  <div class="mr-6">
                    <div class="caption">
                      Weigh-in By
                    </div>
                    <div class="caption font-weight-black">
                      {{ transRecord.WeigherInName }}
                    </div>
                    <div class="caption ">
                      {{ transRecord.DTInbound }}
                    </div>
                  </div>
                  <div>
                    <div class="caption">
                      Weigh-Out By
                    </div>
                    <div class="caption font-weight-bold">
                      {{ transRecord.WeigherOutName }}
                    </div>
                    <div class="caption ">
                      {{ transRecord.DTOutbound }}
                    </div>
                  </div>
                </div>
              </v-col>
            </v-row>
          </v-col>
          <v-col cols="4">
            <v-card tile class="mx-auto">
              <v-card-text>
                <TransWeightDetails :trans-record="transRecord" />
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>
        <TransLimitStatus
          v-model="showLimitStatus"
          :limit-status="transferLimit"
        />
      </v-container>
    </ValidationObserver>
  </v-sheet>
</template>

<script>
import navbarMixins from "@/mixins/navbarMixins.js";
import notiMixin from "@/mixins/notiMixin.js";
import validationMixin from "@/mixins/validationMixin.js";

import { mapState, mapActions } from "vuex";
export default {
  mixins: [navbarMixins, notiMixin, validationMixin],
  props: {
    id: {
      type: Number || String,
      default: 0
    }
  },
  data() {
    return {
      isLoaded: false,
      confirmSave: false,
      transRecord: {}
    };
  },
  computed: {
    ...mapState("transRecord", {
      transferLimit: state => state.transferLimit
    })
  },
  watch: {
    "transRecord.RawMaterialId": function() {
      try {
        this.checkLimit(this.transRecord);
      } catch (error) {
        console.log(error);
      }
    }
  },
  async created() {
    this.setPageTitle();
    try {
      const t = await this.getTransRecord({ ...this.$route.meta, id: this.id });
      this.transRecord = t;
      this.isLoaded = true;
    } catch (error) {
      console.error(error);
    }
  },
  methods: {
    ...mapActions("transRecord", ["getTransRecord", "checkLimit", "save"]),
    setPageTitle() {
      const path = this.$route.path.split("/")[1];
      let r = 0;
      if (path == "weighin") {
        this.pageTitle = "Weigh-In";
      } else if (path == "weighout") {
        this.pageTitle = "Weigh-Out";
      } else if (path == "update") {
        this.pageTitle = "Update Transaction Details";
      }
      this.transactionProcess = r;
    },
    async validateTrans() {
      try {
        return await this.$refs.form.validate();
      } catch (error) {
        console.log(error);
      }
    },
    async preSaveTrans() {
      try {
        const isValid = await this.validateTrans();
        if (!isValid) {
          this.confirmSave = false;
          return;
        } else {
          this.confirmSave = true;
        }
      } catch (error) {
        this.showNotification({
          type: "error",
          content: error
        });
      }
    },
    async saveTrans() {
      try {
        await this.save(this.transRecord);
        // this.close();
      } catch (error) {
        console.log(error.response.status);
        if (error.response.status == 400) {
          const sr = JSON.parse(error.response.data);
          console.log(sr);
          this.$refs.form.setErrors(sr);
        } else {
          this.showNotification({
            type: "error",
            content: error
          });
        }
      }
      // this.close();
    },
    cancel() {
      this.close();
    },
    close() {
      this.$router.back();
    },
    showLimitStatus() {
      return this.transRecord.RawMaterialId != 0;
    }
  }
};
</script>

<style lang="scss">
// .indicator-panel {
//   right: 20px;
//   top: 100px;
// }
.v-label-custom {
  overflow: hidden;
  text-overflow: ellipsis;
  top: 6px;
  white-space: nowrap;
  pointer-events: none;
  color: rgba(0, 0, 0, 0.6);
}
</style>
