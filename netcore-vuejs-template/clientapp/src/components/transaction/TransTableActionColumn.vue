<template>
  <div class="children-mr-2">
    <ButtonIcon btn-icon="mdi-pencil" :to="editAction" />
    <ButtonIcon
      class="error"
      icon-color="white"
      btn-icon="mdi-delete"
      :to="deleteAction"
    />
  </div>
</template>

<script>
export default {
  name: "TransTableActionColumn",
  props: {
    editAction: {
      type: Object,
      default: () => {}
    },
    deleteAction: {
      type: Object,
      default: () => {}
    }
  }
};
</script>

<style></style>
