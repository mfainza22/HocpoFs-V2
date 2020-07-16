<template>
  <div>
    <v-dialog v-model="show" max-width="800" persistent>
      <v-form @submit.prevent="save">
        <v-card class="pa-5">
          <v-card-title>
            Add New Product
            <div v-show="hasInternalError">
              {{ internalErrorMsg }}
            </div>
          </v-card-title>
          <v-card-text>
            <v-row dense>
              <v-col cols="6">
                <v-text-field
                  v-model="item.description"
                  label="Description"
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-combobox
                  v-model="item.category"
                  label="Category"
                  :items="categories"
                  item-text="description"
                  item-value="description"
                  :return-object="false"
                ></v-combobox>
              </v-col>
              <v-col cols="6">
                <v-combobox
                  v-model="item.brand"
                  label="Brand"
                  :items="brands"
                  item-text="description"
                  item-value="description"
                  :return-object="false"
                ></v-combobox>
              </v-col>
              <v-col cols="3">
                <v-text-field
                  v-model="item.initStock"
                  label="Initial Stock"
                  type="number"
                ></v-text-field>
              </v-col>
              <v-col cols="6" class="d-flex">
                <v-checkbox
                  v-model="item.serialized"
                  label="Serialized"
                  class="mr-5"
                ></v-checkbox>
                <v-text-field
                  v-model="item.serialCode"
                  label="Serial/Lot #"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-textarea
                  v-model="item.notes"
                  label="Notes"
                  placeholder="Notes"
                ></v-textarea>
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
import productService from "@/services/productService.js";
import brandService from "@/services/brandService.js";
import categoryService from "@/services/categoryService.js";

export default {
  name: "ProductCreate",
  mixins: [notiMixin],
  props: {
    id: {
      type: Number,
      default: 0
    }
  },
  data() {
    return {
      show: true,
      item: {},
      brands: [],
      categories: [],
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
    this.getCategories();
    this.getBrands();
    if (this.id == 0) {
      this.item = this.defaultItem();
    } else {
      this.item = this.getDetails();
    }
  },
  beforeCreate() {},
  methods: {
    defaultItem() {
      return {
        id: 0,
        description: "",
        category: "",
        brand: "",
        serialCode: "",
        initStock: 0,
        onStock: 0,
        notes: "",
        img: "no-img.png"
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
        await productService.create(this.item);
        noti.type = "success";
        noti.content = "New product added successfully";
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
        await productService.update(this.item.id, this.item);
        noti.type = "success";
        noti.content = "Product updated successfully";
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
    getBrands() {
      return brandService.list().then(response => {
        this.brands = response.data;
      });
    },
    getCategories() {
      return categoryService.list().then(response => {
        this.categories = response.data;
      });
    },
    async getDetails() {
      try {
        const response = await productService.getById(this.id);
        this.item = response.data;
      } catch (error) {
        console.log(error);
      }
    }
  }
};
</script>

<style></style>
