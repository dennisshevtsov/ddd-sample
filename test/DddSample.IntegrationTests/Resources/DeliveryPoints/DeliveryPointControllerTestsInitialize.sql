INSERT INTO warehouse (
    id,
    address,
    contact
)
VALUES (
    '11111111-1111-1111-1111-111111111111'::uuid,

    jsonb_build_object(
        'address', 'Main Warehouse Berlin',
        'coordinates', jsonb_build_object(
            'longitude', 13.4050,
            'latitude', 52.5200
        )
    ),

    jsonb_build_object(
        'emails', jsonb_build_array(
            'warehouse@example.com',
            'support@example.com'
        ),
        'phones', jsonb_build_array(
            '+49123456789',
            '+49987654321'
        )
    )
);

INSERT INTO delivery_point (
    id,
    address,
    opening_hours,
    warehouse_id
)
VALUES (
    '22222222-2222-2222-2222-222222222222'::uuid,

    jsonb_build_object(
        'address', 'Berlin Pickup Point',
        'coordinates', jsonb_build_object(
            'longitude', 13.3889,
            'latitude', 52.5170
        )
    ),

    jsonb_build_object(
        'works_on_holidays', true,
        'mon', '12:00-18:00',
        'tue', '12:00-18:00',
        'wed', '12:00-18:00',
        'thu', '12:00-18:00',
        'fri', '12:00-18:00',
        'sat', '10:00-16:00',
        'sun', null
    ),

    '11111111-1111-1111-1111-111111111111'::uuid
);
