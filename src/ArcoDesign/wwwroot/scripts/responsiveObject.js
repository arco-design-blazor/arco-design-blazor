
let subscribers = [];
let subUid = -1;
let screens = {};

const responsiveMap = {
    xs: '(max-width: 575px)',
    sm: '(min-width: 576px)',
    md: '(min-width: 768px)',
    lg: '(min-width: 992px)',
    xl: '(min-width: 1200px)',
    xxl: '(min-width: 1600px)',
    xxxl: '(min-width: 2000px)',
};

const responsiveObserve = {
    matchHandlers: {},
    dispatch(pointMap, breakpointChecked) {
        screens = pointMap;
        if (subscribers.length < 1) {
            return false;
        }
        subscribers.forEach((item) => {
            item.instance.invokeMethodAsync('ResponsiveHandle', screens, breakpointChecked);
        });
        return true;
    },

    subscribe(instance) {
        if (subscribers.length === 0) {
            this.register();
        }
        const token = (++subUid).toString();
        subscribers.push({
            token,
            instance,
        });
        instance.invokeMethodAsync('ResponsiveHandle', screens, null);
        return token;
    },

    unsubscribe(token) {
        subscribers = subscribers.filter(item => item.token !== token);
        if (subscribers.length === 0) {
            this.unregister();
        }
    },
    unregister() {
        Object.keys(responsiveMap).forEach((screen) => {
            const matchMediaQuery = responsiveMap[screen];
            const handler = this.matchHandlers[matchMediaQuery];
            if (handler && handler.mql && handler.listener) {
                handler.mql.removeListener(handler.listener);
            }
        });
    },
    register() {
        Object.keys(responsiveMap).forEach(screen => {
            const matchMediaQuery = responsiveMap[screen];
            const listener = ({ matches }) => {
                this.dispatch(
                    {
                        ...screens,
                        [screen]: matches,
                    },
                    screen
                );
            };
            const mql = window.matchMedia(matchMediaQuery);
            mql.addListener(listener);
            this.matchHandlers[matchMediaQuery] = {
                mql,
                listener,
            };

            listener(mql);
        });
    }
}


export function subscribe(instance) {
    return responsiveObserve.subscribe(instance);
}

export function unsubscribe(token) {
    responsiveObserve.unsubscribe(token);
}