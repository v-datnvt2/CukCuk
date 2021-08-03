<template>
  <div>
    <button class='button' :class="subclass" @click="$emit('btn-click')" >
      {{ buttonText }}
    </button>
  </div>
</template>
<script>
export default {
  name: "Button",
  props: {
    buttonText: String,
    subclass: String,
  },
};
</script>
