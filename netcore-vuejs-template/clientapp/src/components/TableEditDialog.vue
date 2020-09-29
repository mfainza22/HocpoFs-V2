<template>
  <v-edit-dialog persistent>
    <template v-slot:input>
      <div class="mt-4 title">Shift 2 Limit</div>
      <v-text-field
        ref="text"
        label="Edit"
        single-line
        type="number"
      ></v-text-field>
    </template>
  </v-edit-dialog>
</template>

<script>
export default {
  name: "TableEditDialog",
  props: {},
  data() {
    return {
      show: false
    };
  },
  methods: {
    open() {
      console.log(this.$refs);
      this.$refs.text.focus();
    }
  }
};
</script>

<style></style>
