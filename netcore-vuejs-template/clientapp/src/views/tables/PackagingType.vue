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
                  v-model="item.PackagingTypeDesc"
                  label="Packaging Type"
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
import packTypeService from "@/services/packTypeService.js";

export default {
  name: "PackagingType",
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
      this.modalTitle = "Add New Packaging Type";
    } else {
      this.item = this.getDetails();
      this.modalTitle = "Update Packaging Type";
    }
  },
  beforeCreate() {},
  methods: {
    defaultItem() {
      return {
        id: 0,
        PackagingTypeDesc: ""
      };
    },
    save() {
      if (this.id == 0) {
        this.cancelled = false;
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
        await packTypeService.create(this.item);
        noti.type = "success";
        noti.content = "New Packaging Type added successfully";
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
        await packTypeService.update(this.item.id, this.item);
        noti.type = "success";
        noti.content = "Packaging Type updated successfully";
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
        const response = await packTypeService.getById(this.id);
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
