<template>
    <div>
        <button :class="['button btn-icon', subclass ,iconName ]" @click="$emit('btn-click')">{{buttonText}}</button>
    </div>
</template>
<script>
export default {
    name: 'ButtonIcon',
    props:{
        iconName: String,
        buttonText: String,
        subclass: String,
    }
}
</script>