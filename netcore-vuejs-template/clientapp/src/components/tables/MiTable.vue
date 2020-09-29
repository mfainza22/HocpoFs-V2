<template>
  <div>
    <table>
      <thead>
        <tr></tr>
      </thead>
      <tbody>
        <tr v-for="(item, itemIdx) in filteredItems" :key="itemIdx">
          <template v-for="(header, headerIdx) in headers">
            <slot
              v-if="typeof $scopedSlots[header.value] !== 'undefined'"
              :name="header.value"
              :item="item"
            >
            </slot>
            <td v-else :key="headerIdx">{{ item[header.value] }}</td>
          </template>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  name: "MiTable",
  props: {
    items: {
      type: Array,
      default: () => []
    },
    headers: {
      type: Array,
      default: () => []
    },
    search: {
      type: String,
      default: ""
    }
  },
  data() {
    return {
      itemKeys: []
    };
  },
  computed: {
    filteredItems() {
      const r = this.items;
      console.log(this.search);
      if (this.search != "") {
        return r.filter(a => {
          return a.RawMaterialDesc.includes(this.search);
        });
      }
      return r;
    }
  },
  created() {
    this.init();
  },
  methods: {
    init() {
      console.log(this.headers);
      console.log(this);
    },
    searchText() {}
  }
};
</script>

<style></style>
