<template>
  <div class="pa-10">
    <div v-show="pageLoading">loading</div>
    <div v-show="!pageLoading">
      <v-toolbar class="elevation-1">
        <v-spacer></v-spacer>
        <ConfirmBoxInline
          ref="confirmBox"
          :before-confirm-func="save"
          :cancel-func="cancel"
        />
      </v-toolbar>
      <v-card tile class>
        <v-card-text>
          <v-row>
            <v-col cols="6">
              <AutocompleteVendor v-model="receiving.vendor" />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="6" md="3" sm="6">
              <v-text-field
                v-model="receiving.poNum"
                label="PO Number"
                placeholder=" "
                outlined=""
                dense
              />
            </v-col>
            <v-col cols="6" md="3" sm="6">
              <v-text-field
                v-model="receiving.drNum"
                label="DR Number"
                placeholder=" "
                outlined=""
                dense
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <ReceivingTable />
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </div>
    <router-view />
    <MessageBox ref="messagebox" />
  </div>
</template>

<script>
import navbarMixins from "@/mixins/navbarMixins.js";
import { mapActions } from "vuex";
export default {
  mixins: [navbarMixins],
  data() {
    return {
      pageLoading: true,
      pageTitle: "",
      receiving: {},
      saved: false
    };
  },
  computed: {},
  destroyed() {},

  async created() {
    console.log("CREATED");
    await this.init();
    this.pageLoading = true;
    try {
      if (this.$route.name == "ReceivingCreate") {
        this.pageTitle = "New Receiving";
      } else {
        this.receiving = await this.getById(this.$route.params.id);
        this.pageTitle = "Update Receiving";
      }

      this.pageLoading = false;
    } catch (error) {
      console.log(error);
    }
  },
  methods: {
    ...mapActions("receiving", {
      getById: "getReceivingById",
      init: "initReceiving",
      create: "createReceiving",
      update: "updateReceiving",
      validate: "validateReceiving",
      updateState: "updateState"
    }),
    async save() {
      try {
        const p = await this.$refs.confirmBox.waitConfirm();
        if (p !== true) return;
        this.updateState(this.receiving, this.id);
        const valid = await this.validate();
        if (!valid) return;

        if (typeof this.receiving.id === "undefined") {
          await this.create();
        } else {
          await this.update();
        }
        this.saved = true;
        this.$router.push("/Receiving");
      } catch (e) {
        console.log(e);
      }
    },
    cancel() {
      this.$router.go(-1);
    }
  },
  beforeRouteLeave(to, from, next) {
    if (this.saved) {
      next();
      return;
    }

    if (this.name == to.name) return;

    this.$refs.messagebox.open(
      {
        title: "",
        content: "Any changes made will be discarded. Do you want to proceed?",
        buttons: "yesNo"
      },
      () => {
        next();
      }
    );
  }
};
</script>

<style></style>
